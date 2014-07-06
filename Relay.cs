using UnityEngine;
using System.Collections.Generic;

using HelloThere.InCommon;
using HelloThere.ProgrammableResource;

namespace Relays
{

	public abstract class Relay<TPackage> : PRScriptableObject
	{

		override public void OnGenerate(OperationResultHandler operationResultHandler)
		{
		}
		
		public void AddReceiver(System.Action<object, TPackage> receiver)
		{
			if(HasReceiver(receiver))
				Error("Listener already added on {0}", this);

			receivers.Add(receiver);
		}

		public void RemoveReceiver(System.Action<object, TPackage> receiver)
		{
			if(!HasReceiver(receiver))
				Error("No receiver to remove on {0}", this);

			receivers.Remove(receiver);
		}

		public bool HasReceiver(System.Action<object, TPackage> receiver)
		{
			return receivers.Contains(receiver);
		}

		public void AddTransmission(object transmitter, TPackage package)
		{
			AddTransmission(transmitter, package, RequireReceiver.No);
		}

		public void AddTransmission(object transmitter, TPackage package, RequireReceiver RequireReceiver)
		{
			if(RequireReceiver == RequireReceiver.One && receivers.Count != 1)
				Error("Transmission from {0} required one receiver and there were {1}", transmitter, receivers.Count);

			if(RequireReceiver == RequireReceiver.Yes && receivers.Count == 0)
				Error("Transmission from {0} required receivers and there were none", transmitter);

			transmissions.Enqueue(new Transmission()
			{
				Transmitter = transmitter,
				Package = package
			});
		}

		public void Transmit()
		{
			while(transmissions.Count > 0)
			{
				Transmission transmission = transmissions.Dequeue();

				receivers.ForEach((receiver) => receiver(transmission.Transmitter, transmission.Package));
			}
		}

		private static void Error(string formattedString, params object[] values)
		{
			throw new System.Exception(string.Format(formattedString, values));
		}

		private List<System.Action<object, TPackage>> receivers = new List<System.Action<object, TPackage>>();
		private Queue<Transmission> transmissions = new Queue<Transmission>();

		private struct Transmission
		{
			public object Transmitter;
			public TPackage Package;
		}

	}

}