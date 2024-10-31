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
    /// Describes the target component of an animation curve.
    /// </summary>
    public enum AnimationCurveTarget
    {
        /// <summary>
        /// targets the x component of the pose positon
        /// </summary>
        PositionX,
        /// <summary>
        /// targets the y component of the pose position
        /// </summary>
        PositionY,
        /// <summary>
        /// targets the z component of the pose position
        /// </summary>
        PositionZ,
        /// <summary>
        /// targets the x component of the pose scale
        /// </summary>
        ScaleX,
        /// <summary>
        /// targets the y component of the pose scale
        /// </summary>
        ScaleY,
        /// <summary>
        /// targets the z component of the pose scale
        /// </summary>
        ScaleZ,
        /// <summary>
        /// targets the x component of the pose rotation
        /// </summary>
        RotationX,
        /// <summary>
        /// targets the y component of the pose rotation
        /// </summary>
        RotationY,
        /// <summary>
        /// targets the z component of the pose rotation
        /// </summary>
        RotationZ,
        /// <summary>
        /// targets the w component of the pose rotation
        /// </summary>
        RotationW
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class AnimationCurve : IReadOnlyDictionary<float, float>
    {
        /// <summary>
        /// the target component of the animation curve
        /// </summary>
        public AnimationCurveTarget Target { get; private set; }

        private ImmutableSortedDictionary<float, float> keys;

        /// <summary>
        /// the timess of the curves keyframes
        /// </summary>
        public IEnumerable<float> Keys => keys.Keys;

        /// <summary>
        /// the values of the curves keyframes
        /// </summary>
        public IEnumerable<float> Values => keys.Values;

        /// <summary>
        /// the count of keyframes
        /// </summary>
        public int Count => keys.Count;

        /// <summary>
        /// the values of the animation curves keyframes
        /// </summary>
        /// <param name="key">the time of a keyframe</param>
        /// <returns>returns the value of the matching keyframe</returns>
        public float this[float key] => keys[key];

        /// <summary>
        /// check if the animation contains a keyframe at the given time
        /// </summary>
        /// <param name="key">the time to check for a keyframe</param>
        /// <returns>returns true if there is a keyframe at precisely the given time</returns>
        public bool ContainsKey(float key) => keys.ContainsKey(key);

        /// <summary>
        /// tries to get a value froma a keyframe at the given time
        /// </summary>
        /// <param name="key">the time to to query for a keyframe</param>
        /// <param name="value">the out value for the keyframe</param>
        /// <returns>true if a keyframe value was successfully gotten, false if no such keyframe value can be retrieved</returns>
        public bool TryGetValue(float key, [MaybeNullWhen(false)] out float value) => keys.TryGetValue(key, out value);

        /// <summary>
        /// returns a keyframe enumerator
        /// </summary>
        /// <returns>a keyframe enumerator</returns>
        public IEnumerator<KeyValuePair<float, float>> GetEnumerator() => keys.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// creates a new animation curve from a target collection of keyframes
        /// </summary>
        /// <param name="target">the component target for the animation cure</param>
        /// <param name="input">the keyframes to create the curve from</param>
        public AnimationCurve(AnimationCurveTarget target, ICollection<KeyValuePair<float, float>> input)
        {
            Target = target;
            keys = input.ToImmutableSortedDictionary();
        }

        /// <summary>
        /// samples the curve at the given time, with linear interpolation
        /// </summary>
        /// <param name="x">the time at which to sample the curve</param>
        /// <returns>returns the linearly interpolated value at time x</returns>
        public float Sample(float x)
        {
            KeyValuePair<float, float> a = keys.LastOrDefault(k => k.Key <= x, keys.Last());
            KeyValuePair<float, float> b = keys.FirstOrDefault(k => k.Key >= x, keys.First());
            float alpha = (x - a.Key) / (b.Key - a.Key);
            return a.Key == b.Key ? a.Value : (a.Value * (1.0f - alpha) + b.Value * alpha);
        }
    }
}
