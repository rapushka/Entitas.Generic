#if ENTITAS_GENERIC_UNITY_SUPPORT
using UnityEngine;

namespace Entitas.Generic
{
	public class BehavioursCollector : MonoBehaviour
	{
		[SerializeField] private EntityBehaviourBase[] _behaviours;

		public EntityBehaviourBase[] Behaviours => _behaviours;
	}
}
#endif