#if UNITY_EDITOR
using UnityEngine;

namespace Entitas.Generic
{
	public class EntitiesCollector : MonoBehaviour
	{
		[SerializeField] private EntityBehaviour[] _behaviours;

		public EntityBehaviour[] Behaviours => _behaviours;

		[Button]
		public void CollectAll()
		{
			_behaviours = FindObjectsOfType<EntityBehaviour>();
		}
	}
}
#endif