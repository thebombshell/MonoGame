using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPoseProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<AnimationPose> RequestPose();
    }

    /// <summary>
    /// 
    /// </summary>
    public enum AnimationEndBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        None,
        /// <summary>
        /// 
        /// </summary>
        Stop,
        /// <summary>
        /// 
        /// </summary>
        Loop,
        /// <summary>
        /// 
        /// </summary>
        BackAndforth,
        /// <summary>
        /// 
        /// </summary>
        LoopToZero
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class SimpleAnimator : IPoseProvider, IUpdateable
    {
        /// <summary>
        /// 
        /// </summary>
        public AnimationCollection Animations{ get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="animationName"></param>
        public delegate void OnAnimationDelegate(string animationName);

        /// <summary>
        /// 
        /// </summary>
        public event OnAnimationDelegate OnAnimationEnd;

        /// <summary>
        /// 
        /// </summary>
        public event OnAnimationDelegate OnAnimationChange;

        /// <summary>
        /// 
        /// </summary>
        public string CurrentAnimationName
        {
            get
            {
                return currentAnimationName;
            }
            set
            {
                if (currentAnimationName != value)
                {
                    if (OnAnimationChange != null)
                    {
                        OnAnimationChange(currentAnimationName);
                    }
                    currentAnimationName = !Animations.ContainsKey(value) ? value : null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Animation CurrentAnimation
        {
            get { return CurrentAnimationName == null ? null : Animations[CurrentAnimationName]; }
        }

        private string currentAnimationName = null;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<EventArgs> EnabledChanged;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<EventArgs> UpdateOrderChanged;

        /// <summary>
        /// 
        /// </summary>
        public float Time { get; set; } = 0.0f;

        /// <summary>
        /// 
        /// </summary>
        public float PlaySpeed { get; set; } = 1.0f;

        /// <summary>
        /// 
        /// </summary>
        public bool Playing { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public bool Enabled { get { return true; } }

        /// <summary>
        /// 
        /// </summary>
        public int UpdateOrder { get { return 0; } }

        /// <summary>
        /// 
        /// </summary>
        public AnimationEndBehaviour AnimationEndBehaviour { get; set; } = AnimationEndBehaviour.Stop;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        public void Update(GameTime time)
        {
            Time += (float)time.ElapsedGameTime.TotalSeconds * PlaySpeed;

            if (PlaySpeed > 0.0f && Time > CurrentAnimation.EndTime ||
                PlaySpeed < 0.0f && Time < CurrentAnimation.StartTime)
            {
                if (OnAnimationEnd != null)
                {
                    OnAnimationEnd(currentAnimationName);
                }
                switch (AnimationEndBehaviour)
                {
                    case AnimationEndBehaviour.None:
                        break;
                    case AnimationEndBehaviour.Stop:
                        Playing = false;
                        break;
                    case AnimationEndBehaviour.Loop:
                        Time = CurrentAnimation.StartTime;
                        break;
                    case AnimationEndBehaviour.BackAndforth:
                        PlaySpeed = -PlaySpeed;
                        break;
                    case AnimationEndBehaviour.LoopToZero:
                        Time = 0.0f;
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<AnimationPose> RequestPose()
        {
            return Task.Run(() => { return CurrentAnimation.Sample(Time); });
        }
    }
