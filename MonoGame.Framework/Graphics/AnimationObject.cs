using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// a single objects single animation data
    /// </summary>
    public sealed class AnimationObject
    {
        /// <summary>
        /// the name of the object this animation object animates
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The earliest timestamp in this object
        /// </summary>
        public float StartTime { get; private set; }

        /// <summary>
        /// The latest timestamp in this object
        /// </summary>
        public float EndTime { get; private set; }

        /// <summary>
        /// The total time in seconds between the start and end timestamps in this object
        /// </summary>
        public float Duration { get { return EndTime - StartTime; } }

        /// <summary>
        /// the animation curve data of this animation object
        /// </summary>
        public AnimationCurve[] Curves { get; private set; }

        /// <summary>
        /// creates an animation object with the given name and animation curves
        /// </summary>
        /// <param name="name">the name of the object this animation object animates</param>
        /// <param name="curves">the animation curve data of this animation object</param>
        public AnimationObject(string name, ICollection<AnimationCurve> curves)
        {
            HashSet<AnimationCurveTarget> targets = new HashSet<AnimationCurveTarget>();
            foreach (var curve in curves)
            {
                if (targets.Contains(curve.Target))
                {
                    throw new ArgumentException("There should only be 1 of each animation target within an animation object");
                }
                targets.Add(curve.Target);
            }
            Name = name;
            Curves = curves.ToArray();
        }

        /// <summary>
        /// Generates the world matrix of the animation object at the given time
        /// </summary>
        /// <param name="x">the time at which to sample the animation</param>
        /// <returns>the parent relative world matrix of the animation object a tthe given time</returns>
        public Matrix Sample(float x)
        {
            Vector3 position = Vector3.Zero;
            Vector3 scale = Vector3.One;
            Quaternion rotation = Quaternion.Identity;

            foreach (var curve in Curves)
            {
                switch (curve.Target)
                {
                    case AnimationCurveTarget.PositionX:
                        position.X = curve.Sample(x); break;
                    case AnimationCurveTarget.PositionY:
                        position.Y = curve.Sample(x); break;
                    case AnimationCurveTarget.PositionZ:
                        position.Z = curve.Sample(x); break;
                    case AnimationCurveTarget.ScaleX:
                        scale.X = curve.Sample(x); break;
                    case AnimationCurveTarget.ScaleY:
                        scale.Y = curve.Sample(x); break;
                    case AnimationCurveTarget.ScaleZ:
                        scale.Z = curve.Sample(x); break;
                    case AnimationCurveTarget.RotationX:
                        rotation.X = curve.Sample(x); break;
                    case AnimationCurveTarget.RotationY:
                        rotation.Y = curve.Sample(x); break;
                    case AnimationCurveTarget.RotationZ:
                        rotation.Z = curve.Sample(x); break;
                    case AnimationCurveTarget.RotationW:
                        rotation.W = curve.Sample(x); break;
                }
            }
            return
                Matrix.CreateFromQuaternion(rotation) *
                Matrix.CreateScale(scale) *
                Matrix.CreateTranslation(position);
        }
    }
}
