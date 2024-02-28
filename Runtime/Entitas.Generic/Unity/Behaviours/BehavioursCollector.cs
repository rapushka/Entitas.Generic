using JetBrains.Annotations;
using UnityEngine;

namespace Entitas.Generic
{
	public class BehavioursCollector : MonoBehaviour
	{
		[SerializeField] private EntityBehaviourBase[] _behaviours;

		[PublicAPI]
		public EntityBehaviourBase[] Behaviours => _behaviours;
	}
}