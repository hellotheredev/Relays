using HelloThere.InCommon;

using Relays;

namespace Views
{

	public class ViewRelay : Relay<ViewPackage>
	{

		public void AddViewTransmission(View view, ViewEvent viewEvent, bool instantTransition)
		{
			AddTransmission(this, new ViewPackage()
			{
				View = view,
				Event = viewEvent,
				InstantTransition = instantTransition
			},
			RequireReceiver.No);
		}

	}

	public struct ViewPackage
	{

		public View View;
		public ViewEvent Event;
		public bool InstantTransition;

	}

	public enum ViewEvent
	{

		Show,
		Hide,
		HideAll

	}

}