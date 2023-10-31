using System;
using Entitas.Generic;

namespace Sample
{
	public class GameScope : Attribute, IScope { }

	/// <summary>
	/// If you wanna be closer to classic Entitas
	/// Otherwise feel free to use just Matcher&lt;TScope&gt;
	/// </summary>
	public class GameMatcher : Matcher<GameScope> { }

	public class InputScope : Attribute, IScope { }
}