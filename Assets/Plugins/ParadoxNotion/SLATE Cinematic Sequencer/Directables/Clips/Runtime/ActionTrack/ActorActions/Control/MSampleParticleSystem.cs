using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace Slate.ActionClips
{

    [Category("Control")]
    public class m_SampleParticleSystem : ActorActionClip
    {

        [SerializeField, HideInInspector]
        private float _length = 1f;

        [Required]
        public ParticleSystem particles;
        [Tooltip("If true, then particles simulation will be time synced with the cutscene, otherwise simulation will take place independently")]
        public bool simulationSync = true;

        private ParticleSystem.EmissionModule em;

        public override string info {
            get { return string.Format("Particles ({0})\n{1}", particles && loop ? "Looping" : "OneShot", particles ? particles.gameObject.name : "NONE"); }
        }

        [Button]
        public void GetGroupActor()
        {
            particles = actor.GetComponent<ParticleSystem>();
        }

        public override bool isValid {
            get { return particles != null; }
        }

        public override float length {
            get { return particles == null || loop ? _length : duration + startLifetime; }
            set { _length = value; }
        }

        public override float blendOut {
            get { return isValid && !loop ? startLifetime : 0.1f; }
        }

        private bool loop {
#if UNITY_5_5_OR_NEWER
            get { return particles != null && particles.main.loop; }
#else
			get {return particles != null && particles.loop;}
#endif
        }

        private float duration {
#if UNITY_5_5_OR_NEWER
            get { return particles != null ? particles.main.duration : 0f; }
#else
			get {return particles != null? particles.duration : 0f;}
#endif
        }

        private float startLifetime {
#if UNITY_5_5_OR_NEWER
            get { return particles != null ? particles.main.startLifetimeMultiplier : 0f; }
#else
			get {return particles != null? particles.startLifetime : 0f;}
#endif
        }

        private ParticleSystem tempParticle;
        protected override void OnEnter()
        {
//by:zihao.zhang---start
            if (actor!=null)
            {
                tempParticle = particles;
                particles = actor.GetComponent<ParticleSystem>();
            }
//by:zihao.zhang---end
            Play();
        }
        protected override void OnReverseEnter() { Play(); }
        protected override void OnExit() { Stop(); }
        protected override void OnReverse() { Stop(); }

        protected override void OnRootEnabled() {
            em = particles.emission;
            em.enabled = false;
            particles.Stop();
        }

        protected override void OnRootDisabled() {
            em = particles.emission;
            em.enabled = true;
        }

        void Play() {
            if ( !particles.isPlaying ) {
//by:zihao.zhang---start
                if (particles == actor)
//by:zihao.zhang---end
                {
                    particles.useAutoRandomSeed = false;
                }
            }
            em = particles.emission;
            em.enabled = true;
            particles.Play();
        }


        protected override void OnUpdate(float time) {
//by:zihao.zhang--start
            if (actor!=particles)
            {
                particles = actor.GetComponent<ParticleSystem>();
            }
//by:zihao.zhang---end
            if ( !Application.isPlaying) {
                em.enabled = time < length;
                if ( simulationSync ) {
                    particles.Simulate(time);
                }
            }

        }

        void Stop() {
//by:zihao.zhang--start
            particles = tempParticle;
//by:zihao.zhang---end
            em.enabled = false;
            particles.Stop();
            
        }
    }
}