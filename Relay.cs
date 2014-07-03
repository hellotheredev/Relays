using UnityEngine;
using System.Collections.Generic;

using HelloThere.InCommon;
using HelloThere.ProgrammableResource;

namespace Relays
{

	public abstract class Relay<TPackage> : PRScriptableObject
	{

		public void AddReciever(System.Action<object, TPackage> receiver)
		{
			if(HasReceiver(receiver))
				Error("Listener already added on {0}", this);

			recievers.Add(receiver);
		}

		public void RemoveReceiver(System.Action<object, TPackage> receiver)
		{
			if(!HasReceiver(receiver))
				Error("No receiver to remove on {0}", this);

			recievers.Remove(receiver);
		}

		public bool HasReceiver(System.Action<object, TPackage> receiver)
		{
			return recievers.Contains(receiver);
		}

		public void AddTransmission(object transmitter, TPackage package)
		{
			AddTransmission(transmitter, package, RequireReceivers.No);
		}

		public void AddTransmission(object transmitter, TPackage package, RequireReceivers requireReceivers)
		{
			if(requireReceivers == RequireReceivers.One && recievers.Count != 1)
				Error("Transmission from {0} required one receiver and there were {1}", transmitter, recievers.Count);

			if(requireReceivers == RequireReceivers.Yes && recievers.Count == 0)
				Error("Transmission from {0} required recievers and there were none", transmitter);

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

				recievers.ForEach((receiver) => receiver(transmission.Transmitter, transmission.Package));
			}
		}

		private static void Error(string formattedString, params object[] values)
		{
			throw new System.Exception(string.Format(formattedString, values));
		}

		private List<System.Action<object, TPackage>> recievers = new List<System.Action<object, TPackage>>();
		private Queue<Transmission> transmissions = new Queue<Transmission>();

		private struct Transmission
		{
			public object Transmitter;
			public TPackage Package;
		}

	}

}