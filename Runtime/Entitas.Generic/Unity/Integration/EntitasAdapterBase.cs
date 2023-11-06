using UnityEngine;

namespace Entitas.Generic
{
	public abstract class EntitasAdapterBase : MonoBehaviour
	{
		private void Awake() => Initialize(Contexts.Instance);

		protected abstract void Initialize(Contexts contexts);
	}
}