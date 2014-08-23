using UnityEngine;
using UnityEngine.EventSystems;

using HelloThere.InCommon;
using HelloThere.ProgrammableValidation;

using Relays;
using Sequences;

namespace Relays.UI
{

	public abstract class UIElement : PVMonoBehaviour,
	IPointerClickHandler, IPointerDownHandler, IPointerUpHandler,
	IPointerEnterHandler, IPointerExitHandler
	{



		public UIRelay relay;
		public RequireReceiver requireReceiver;

		private Animator animator;

		#region PVMonoBehaviour implementation

		protected override void OnValidation (OperationResultHandler resultHandler)
		{
			AssertField (relay, "Relay");

			animator = GetElement<Animator> ();

			//onPointerClickSequence = GetElement<SequencePlayer> ("*/Sequences/Pointer/Click");
			//onPointerDownSequence = GetElement<SequencePlayer> ("*/Sequences/Pointer/Down");
			//onPointerUpSequence = GetElement<SequencePlayer> ("*/Sequences/Pointer/Up");
			//onPointerEnterSequence = GetElement<SequencePlayer> ("*/Sequences/Pointer/Enter");
			//onPointerExitSequence = GetElement<SequencePlayer> ("*/Sequences/Pointer/Exit");

			OnValidateUIElement (resultHandler);
		}

		protected abstract void OnValidateUIElement (OperationResultHandler resultHandler);

		#endregion

		#region EventSystems -> Animator

		public virtual void OnPointerClick (PointerEventData eventData)
		{
			if (!enabled)
				return;
		}

		public virtual void OnPointerDown (PointerEventData eventData)
		{
			if (!enabled)
				return;

			animator.SetTrigger ("Pressed");
		}

		public virtual void OnPointerUp (PointerEventData eventData)
		{
			if (!enabled)
				return;

			animator.SetTrigger ("Normal");
		}

		public virtual void OnPointerEnter (PointerEventData eventData)
		{
			if (!enabled)
				return;
		}

		public virtual void OnPointerExit (PointerEventData eventData)
		{
			if (!enabled)
				return;

			animator.SetTrigger ("Normal");
		}

		#endregion

		protected virtual void OnEnable ()
		{
			animator.SetTrigger ("Normal");
		}

		protected virtual void OnDisable ()
		{
			animator.SetTrigger ("Disabled");
		}

	}

}