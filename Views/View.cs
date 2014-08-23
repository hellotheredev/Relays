using UnityEngine;

using HelloThere.InCommon;
using HelloThere.ProgrammableValidation;

using Sequences;
using Relays;

namespace Views
{

	public abstract class View : PVMonoBehaviour, IDisposableObject
	{

		#region PVMonoBehaviour implementation

		protected override void OnValidation (OperationResultHandler resultHandler)
		{
			gameObject.name = GetType ().ToString ();

			if ((elements = GetElement<Canvas> ("*/Elements")) != null) {
				elements.renderMode = RenderMode.Overlay;
			}
			    
			transitionIn = GetElement<SequencePlayer> ("*/Transitions/In");
			transitionOut = GetElement<SequencePlayer> ("*/Transitions/Out");

			OnValidateView (resultHandler);
		}

		protected abstract void OnValidateView (OperationResultHandler resultHandler);

		#endregion

		#region IDisposableObject implementation

		public void Dispose ()
		{
			isDisposed = true;

			OnDisposeView ();
		}

		protected abstract void OnDisposeView ();

		public bool isDisposed { get; private set; }

		#endregion

		public void Show (bool instantTransition)
		{
			elements.enabled = true;

			Transition (transitionIn, instantTransition, null);

			OnShow ();
		}

		public void Hide (bool instantTransition)
		{
			OnHide ();

			Transition (transitionOut, instantTransition, () => {
				elements.enabled = false;
			});
		}

		private void Transition (SequencePlayer transition, bool instant, System.Action onComplete)
		{
			transitionIn.Stop ();
			transitionOut.Stop ();

			if (instant) {
				transition.SampleEnd ();
				
				if (onComplete != null)
					onComplete ();
			} else {
				transition.SampleStart ();
				transition.Play (onComplete);
			}
		}

		private Canvas elements;
		private SequencePlayer transitionIn;
		private SequencePlayer transitionOut;

		protected abstract void OnShow ();

		protected abstract void OnHide ();

	}

}