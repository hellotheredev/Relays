using System;

using UnityEngine;

using HelloThere.InCommon;
using HelloThere.ProgrammableValidation;

using Relays;

namespace Views
{

	using ViewDictionary = System.Collections.Generic.Dictionary<Type, View>;

	public class ViewCollection : PVMonoBehaviour, IDisposableObject
	{

		private ViewDictionary viewDictionary = new ViewDictionary ();

		#region PVMonoBehaviour implementation

		protected override void OnValidation (OperationResultHandler resultHandler)
		{
			viewDictionary.Clear ();

			foreach (View view in GetComponentsInChildren<View>()) {
				Type viewType = view.GetType ();

				if (AssertTrue (!viewDictionary.ContainsKey (viewType), "There can only be one {0}.", viewType.ToString ())) {
					viewDictionary.Add (view.GetType (), view);
				}
			}
		}

		#endregion

		#region IDisposableObject implementation

		public void Dispose ()
		{
			isDisposed = true;

			foreach (View view in viewDictionary.Values) {
				view.Dispose ();
			}
		}

		public bool isDisposed { get; private set; }

		#endregion

		private void Start ()
		{
			HideAll (true);
		}

		#region Control views

		private bool HasView<TView> ()
		{
			return viewDictionary.ContainsKey (typeof(TView));
		}

		public bool TryGetView<TView> (out TView view) where TView : View
		{
			Type viewType = typeof(TView);

			if (HasView<TView> ()) {
				view = viewDictionary [viewType] as TView;
				return true;
			} else {
				view = default(TView);
				return false;
			}
		}

		public TView Show<TView> () where TView : View
		{
			return Show<TView> (false);
		}

		public TView Show<TView> (bool instantTransition) where TView : View
		{
			TView view = default(TView);

			if (TryGetView<TView> (out view)) {
				view.Show (instantTransition);
			} else {
				Debug.LogError ("There are no views of type " + typeof(TView).ToString ());
			}

			return view;
		}

		public void Hide<TView> () where TView : View
		{
			Hide<TView> (false);
		}

		public void Hide<TView> (bool instantTransition) where TView : View
		{
			TView view;

			if (TryGetView<TView> (out view)) {
				view.Hide (instantTransition);
			} else {
				Debug.LogError ("There are no views of type " + typeof(TView).ToString ());
			}
		}

		public void HideAll ()
		{
			HideAll (false);
		}

		public void HideAll (bool instantTransition)
		{
			foreach (View view in viewDictionary.Values) {
				view.Hide (instantTransition);
			}
		}

		#endregion

	}

}