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
    /// A single total animation
    /// </summary>
    public sealed class Animation : IReadOnlyDictionary<string, AnimationObject>
    {
        /// <summary>
        /// name of the animation
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// the animation objects
        /// </summary>
        public ImmutableDictionary<string, AnimationObject> Objects { get; private set; }

        /// <summary>
        /// animation object of matching name
        /// </summary>
        /// <param name="key">name of an animation object</param>
        /// <returns>an animation object</returns>
        public AnimationObject this[string key] => Objects[key];

        /// <summary>
        /// keyed animation object names
        /// </summary>
        public IEnumerable<string> Keys => Objects.Keys;

        /// <summary>
        /// keyed animation objects
        /// </summary>
        public IEnumerable<AnimationObject> Values => Objects.Values;

        /// <summary>
        /// count of keyed animation objects
        /// </summary>
        public int Count => Objects.Count;

        /// <summary>
        /// returns true if a keyed animation object exists with the given name
        /// </summary>
        /// <param name="key">the name of an aniamtion object</param>
        /// <returns>true if a keyed animation object exists with the given name</returns>
        public bool ContainsKey(string key) => Objects.ContainsKey(key);

        /// <summary>
        /// returns an enumerator of the keyed animaiton objects
        /// </summary>
        /// <returns>an enumerator of the keyed animation objects</returns>
        public IEnumerator<KeyValuePair<string, AnimationObject>> GetEnumerator() => Objects.GetEnumerator();

        /// <summary>
        /// try to get the animation object of the given name
        /// </summary>
        /// <param name="key">the name to query for an animation object</param>
        /// <param name="value">the out value aniamtion object</param>
        /// <returns>true if the animation object is output successfully, false if no matching animation object could be found</returns>
        public bool TryGetValue(string key, [MaybeNullWhen(false)] out AnimationObject value) => Objects.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
