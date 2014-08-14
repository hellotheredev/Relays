using UnityEngine;

using HelloThere.InCommon;
using HelloThere.ProgrammableValidation;

using Sekund;
using Relays;

namespace Views
{

	public abstract class View : PVMonoBehaviour
	{

		protected override void OnValidation(OperationResultHandler resultHandler)
		{
			gameObject.name = GetType().ToString();

			Camera = GetElement<Camera>();

			transitionIn = GetElement<CommonPlaybackBehaviour>("*/Transitions/In");
			transitionOut = GetElement<CommonPlaybackBehaviour>("*/Transitions/Out");

			if(transitionIn != null && transitionIn is SekundPlayer)
				(transitionIn as SekundPlayer).filterRoot = transform;

			if(transitionOut != null && transitionOut is SekundPlayer)
				(transitionOut as SekundPlayer).filterRoot = transform;

			if(AssertField<ViewRelay>(Relay, "Relay") && !Relay.HasReceiver(OnTransmission))
			{
				Relay.AddReceiver(OnTransmission);
			}

			OnValidateView(resultHandler);
		}

		protected abstract void OnValidateView(OperationResultHandler resultHandler);

		private void OnTransmission(object transmitter, ViewPackage package)
		{
			if(package.Event == ViewEvent.HideAll)
			{
				Hide(package.InstantTransition);
			}
			else
			{
				if(package.View == this)
				{
					if(package.Event == ViewEvent.Show) Show(package.InstantTransition);
					if(package.Event == ViewEvent.Hide) Hide(package.InstantTransition);
				}
			}
		}

		public void Show(bool instantTransition)
		{
			Camera.enabled = true;

			Transition(transitionIn, instantTransition, null);

			OnShow();
		}

		public void Hide(bool instantTransition)
		{
			OnHide();

			Transition(transitionOut, instantTransition, () =>{ camera.enabled = false; });
		}

		private void Transition(ICommonPlayback transition, bool instant, System.Action onComplete)
		{
			transitionIn.Cancel();
			transitionOut.Cancel();

			if(instant)
			{
				transition.normalizedPosition = 1;
				
				if(onComplete != null)
					onComplete();
			}
			else
			{
				transition.position = 0;
				transition.Play(onComplete);
			}
		}

		[HideInInspector]
		public	Camera Camera;

		private CommonPlaybackBehaviour transitionIn;
		private CommonPlaybackBehaviour transitionOut;

		protected abstract void OnShow();
		protected abstract void OnHide();

		public ViewRelay Relay;

	}

}