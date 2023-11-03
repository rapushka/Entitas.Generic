#if UNITY_EDITOR
using System.Linq;
using UnityEngine;

namespace Entitas.Generic
{
	public class BehavioursCollector : MonoBehaviour
	{
		[SerializeField] private EntityBehaviour[] _behaviours;

		public EntityBehaviour[] Behaviours => _behaviours;

		[Button]
		public void CollectAll()
		{
			_behaviours = FindObjectsOfType<EntityBehaviour>()
			              .Where((e) => e is not ISelfRegistry)
			              .ToArray();
		}
	}
}
#endif