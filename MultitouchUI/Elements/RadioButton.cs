/// TODO: add disabled state

using UnityEngine;

using HelloThere.InCommon;
using HelloThere.ProgrammableValidation;
using HelloThere.MultiTouch;

using Sequences;
using Relays;

namespace MultitouchUI
{

	public class RadioButton : UIElement
	{

		protected override void OnValidation(OperationResultHandler operationResultHandler)
		{
			base.OnValidation(operationResultHandler);

			OnSelected = GetElement<SequencePlayer>("Sequences/Input/OnSelected");
			OnDeselected = GetElement<SequencePlayer>("Sequences/Input/OnDeselected");
		}

		void OnEnable()
		{
			Relay.AddReceiver(OnTransmission);

			OnDeselected.SampleEnd();
			OnSelected.SampleStart();
		}

		void OnDisable()
		{
			Relay.RemoveReceiver(OnTransmission);
		}

		override protected void OnReleaseInside(FingerSession fingerSession)
		{
			base.OnReleaseInside(fingerSession);

			Relay.AddUITransmission(this, UIEvents.Selected, Option, RequireReceiver);
		}

		void OnTransmission(object transmitter, UIPackage package)
		{
			SequencePlayer sequencePlayer = null;

			if(package.Event == UIEvents.Selected)
			{
				if(transmitter == this)
				{
					sequencePlayer = OnSelected;
				}
				else
				{
					sequencePlayer = OnDeselected;

					Relay.AddUITransmission(this, UIEvents.Deselected, Option);
				}

				if(package.ImmediateStateChange)
				{
					sequencePlayer.SampleEnd();
				}
				else
				{
					sequencePlayer.Play();
				}
			}
		}

		private SequencePlayer OnSelected;
		private SequencePlayer OnDeselected;

		public ScriptableObject Option;

	}

}