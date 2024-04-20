#if UNITY
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