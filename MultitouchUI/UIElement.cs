using UnityEngine;
using System.Collections;

using HelloThere.InCommon;
using HelloThere.ProgrammableValidation;
using HelloThere.MultiTouch;
using Relays;
using Sequences;

namespace MultitouchUI
{

	public class UIElement : InputListener
	{

		protected override void OnValidation(OperationResultHandler pResultHandler)
		{
			AssertField(Relay, "Relay");

			OnPressedSequencePlayer = GetElement<SequencePlayer>("Sequences/Input/OnPressed");
			OnReleaseInsideSequencePlayer = GetElement<SequencePlayer>("Sequences/Input/OnReleaseInside");
			OnReleaseOutsideSequencePlayer = GetElement<SequencePlayer>("Sequences/Input/OnReleaseOutside");
			OnSlideInSequencePlayer = GetElement<SequencePlayer>("Sequences/Input/OnSlideIn");
			OnSlideOutSequencePlayer = GetElement<SequencePlayer>("Sequences/Input/OnSlideOut");
		}

		void OnEnable()
		{
			OnSlideOutSequencePlayer.SampleEnd();
			OnSlideInSequencePlayer.SampleStart();
			OnReleaseOutsideSequencePlayer.SampleEnd();
			OnReleaseInsideSequencePlayer.SampleStart();
			OnPressedSequencePlayer.SampleStart();
		}

		override protected void OnPressed(FingerSession fingerSession)
		{
			base.OnPressed(fingerSession);

			OnPressedSequencePlayer.Play();
		}

		override protected void OnReleaseInside(FingerSession fingerSession)
		{
			base.OnReleaseInside(fingerSession);

			OnReleaseInsideSequencePlayer.Play();
		}

		override protected void OnReleaseOutside(FingerSession fingerSession)
		{
			base.OnReleaseOutside(fingerSession);

			OnReleaseOutsideSequencePlayer.Play();
		}

		override protected void OnSlideIn(FingerSession fingerSession)
		{
			base.OnSlideIn(fingerSession);

			OnSlideInSequencePlayer.Play();
		}

		override protected void OnSlideOut(FingerSession fingerSession)
		{
			base.OnSlideOut(fingerSession);

			OnSlideOutSequencePlayer.Play();
		}

		public UIRelay Relay;
		public RequireReceiver RequireReceiver;

		private SequencePlayer OnPressedSequencePlayer;
		private SequencePlayer OnReleaseInsideSequencePlayer;
		private SequencePlayer OnReleaseOutsideSequencePlayer;
		private SequencePlayer OnSlideInSequencePlayer;
		private SequencePlayer OnSlideOutSequencePlayer;

	}

}