// ReSharper disable InconsistentNaming
using System;

namespace Entitas.Generic
{
	public class AERCFactories
	{
		public static readonly Func<IEntity, IAERC> UnsafeAERCFactory = (_) => new UnsafeAERC();
		public static readonly Func<IEntity, IAERC> SafeAERCFactory = (e) => new SafeAERC(e);
	}
}