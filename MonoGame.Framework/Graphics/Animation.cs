using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Animation : IReadOnlyDictionary<string, AnimationObject>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public ImmutableDictionary<string, AnimationObject> Objects { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public AnimationObject this[string key] => Objects[key];

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<string> Keys => Objects.Keys;

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<AnimationObject> Values => Objects.Values;

        /// <summary>
        /// 
        /// </summary>
        public int Count => Objects.Count;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key) => Objects.ContainsKey(key);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<string, AnimationObject>> GetEnumerator() => Objects.GetEnumerator();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(string key, [MaybeNullWhen(false)] out AnimationObject value) => Objects.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
