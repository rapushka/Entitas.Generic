using JetBrains.Annotations;
using UnityEngine;

namespace Entitas.Generic
{
	public class BehavioursCollector : MonoBehaviour
	{
		[SerializeField] private EntityBehaviour[] _behaviours;

		[PublicAPI]
		public EntityBehaviour[] Behaviours => _behaviours;
	}
}