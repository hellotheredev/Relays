using System;

using UnityEngine;

using HelloThere.InCommon;
using HelloThere.ProgrammableValidation;

using Relays;

namespace Views
{

	using ViewDictionary = System.Collections.Generic.Dictionary<Type, View>;

	public class ViewCollection : PVMonoBehaviour
	{

		protected override void OnValidation(OperationResultHandler resultHandler)
		{
			viewDictionary = new ViewDictionary();

			AssertField<ViewRelay>(Relay, "View Relay");

			foreach(View view in GetComponentsInChildren<View>())
			{
				if(view.Relay != Relay)
				{
					Debug.LogWarning(string.Format("Changed Relay of {0} to {1}.", view.name, Relay.name));

					view.Relay = Relay;
				}

				Type viewType = view.GetType();

				if(AssertTrue(!viewDictionary.ContainsKey(viewType), "There can only be one {0}.", viewType.ToString()))
				{
					viewDictionary.Add(view.GetType(), view);
				}
			}
		}

		private void Start()
		{
			HideAll(true);
		}

		private void AssertHasView(Type viewType)
		{
			if(!viewDictionary.ContainsKey(viewType))
			{
				throw new Exception("There are no views of type: " + viewType.ToString());
			}
		}

		public void Show<TView>()
		{
			Show<TView>(false);
		}

		public void Show<TView>(bool instantTransition)
		{
			Type viewType = typeof(TView);

			AssertHasView(viewType);

			Relay.AddViewTransmission(viewDictionary[viewType], ViewEvent.Show, instantTransition);
			Relay.Transmit();
		}

		public void Hide<TView>()
		{
			Hide<TView>(false);
		}

		public void Hide<TView>(bool instantTransition)
		{
			Type viewType = typeof(TView);

			AssertHasView(viewType);

			Relay.AddViewTransmission(viewDictionary[viewType], ViewEvent.Hide, instantTransition);
			Relay.Transmit();
		}

		public void HideAll()
		{
			HideAll(false);
		}

		public void HideAll(bool instantTransition)
		{
			Relay.AddViewTransmission(null, ViewEvent.HideAll, instantTransition);
			Relay.Transmit();
		}

		private ViewDictionary viewDictionary;

		public ViewRelay Relay;

	}

}