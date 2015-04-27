using System;
using System.Collections.Generic;
using System.Reflection;
using Trite.Cement.Link;
using Trite.Cement.Contexts;

namespace Trite.Cement
{
    public class Implementations
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Implementations"/> class.
        /// </summary>
        public Implementations() : this(ContextFactory.CreateApplicationAssemblyContext())
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Implementations"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Implementations(IAssemblyContext context)
        {
            this._cacheFlags = new Dictionary<Type, bool>();
            this._linkers = new Dictionary<Type, object>();

            this._context = context;
        }
        #endregion

        #region Fields
        private readonly Dictionary<Type, bool> _cacheFlags;
        private readonly Dictionary<Type, object> _linkers;

        private IAssemblyContext _context;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        public IAssemblyContext Context
        {
            get { return this._context; }
            set
            {
                this._context = value;

                lock (this._cacheFlags)
                    this._cacheFlags.Clear();              
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initializes the linker for the specified key and value combination.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        private Linker<TKey, TValue> GetLinker<TKey, TValue>() where TValue : ILinkable<TKey>
        {
            lock (this._linkers)
            {
                Linker<TKey, TValue> linker;

                if (this._linkers.ContainsKey(typeof(TValue)))
                {
                    linker = (Linker<TKey, TValue>)this._linkers[typeof(TValue)];
                }
                else
                {
                    this._linkers.Add(typeof(TValue), (linker = new Linker<TKey, TValue>() { DuplicateBehavior = DuplicateBehavior.Override }));
                }

                return linker;
            }
        }
        /// <summary>
        /// Resolves the specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="creationType">Type of the creation.</param>
        private TValue Get<TKey, TValue>(TKey key, CreationType creationType) where TValue : ILinkable<TKey>
        {
            if (!this.IsCached<TValue>())
            {
                this.Cache<TKey, TValue>();
            }

            Linker<TKey, TValue> linker = this.GetLinker<TKey, TValue>();
            linker.CreationType = creationType;

            return linker.Resolve(key);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds all implementations from the specified assembly context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Add(IAssemblyContext context)
        {
            this.Context = new MergedAssemblyContext(this.Context, context);
        }
        /// <summary>
        /// Adds all implementations from the assembly of the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void AddFromAssemblyOf<T>()
        {
            this.Add(ContextFactory.CreateSingleAssemblyContext(typeof(T)));
        }
        /// <summary>
        /// Registers the specified value.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="value">The value.</param>
        public void Add<TValue>(TValue value) where TValue : ILinkable<Type>
        {
            this.Add<Type, TValue>(value);
        }
        /// <summary>
        /// Registers the specified value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="value">The value.</param>
        public void Add<TKey, TValue>(TValue value) where TValue : ILinkable<TKey>
        {
            Linker<TKey, TValue> linker = this.GetLinker<TKey, TValue>();
            linker.Add(value);
        }
        /// <summary>
        /// Gets the singleton instance for the specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        public TValue Get<TKey, TValue>(TKey key) where TValue : ILinkable<TKey>
        {
            return this.Get<TKey, TValue>(key, CreationType.Singleton);
        } 
        /// <summary>
        /// Gets a new instance for the specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        public TValue GetNew<TKey, TValue>(TKey key) where TValue : ILinkable<TKey>
        {
            return this.Get<TKey, TValue>(key, CreationType.New);
        }
        /// <summary>
        /// Gets the type for the specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        public Type GetType<TKey, TValue>(TKey key) where TValue : ILinkable<TKey>
        {
            return this.Get<TKey, TValue>(key).GetType();
        }
        /// <summary>
        /// Resolves all instances for the specified type.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        public IEnumerable<TValue> All<TKey, TValue>() where TValue : ILinkable<TKey>
        {
            if (!this.IsCached<TValue>())
            {
                this.Cache<TKey, TValue>();
            }

            return (IEnumerable<TValue>)this._linkers[typeof(TValue)];
        }
        /// <summary>
        /// Returns a value that determines wether the specified context was already cached or not.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        public bool IsCached<TValue>()
        {
            return this._cacheFlags.ContainsKey(typeof (TValue));
        }
        /// <summary>
        /// Caches the specified context.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        public void Cache<TKey, TValue>() where TValue : ILinkable<TKey>
        {            
            lock (this._cacheFlags)
            {
                if (!this.IsCached<TValue>())
                {
                    var linker = this.GetLinker<TKey, TValue>();
                    foreach (Assembly assembly in this._context.Assemblies)
                    {
                        linker.Load(assembly);
                    }

                    this._cacheFlags.Add(typeof(TValue), true);
                }
            }
        }
        /// <summary>
        /// Removes the item with the specified identifier from the corresponding linker.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        public void Remove<TKey, TValue>(TKey id) where TValue : ILinkable<TKey>
        {
            Linker<TKey, TValue> linker = this.GetLinker<TKey, TValue>();
            linker.Remove(id);
        }
        #endregion
    }
}
