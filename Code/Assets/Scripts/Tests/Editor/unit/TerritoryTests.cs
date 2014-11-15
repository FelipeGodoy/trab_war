using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UnityEngine;
using NSubstitute;

[TestFixture]
[Category("Goals Test")]
internal class TerritoryTests{

	[Test]
	public void HaveNotNeighborEnemy(){
		var player = Substitute.For<Player>();
		var otherPlayer = Substitute.For<Player>();
		var t1 = Substitute.For<Territory>();
		var t2 = Substitute.For<Territory>();
		t1.CurrentPlayer = player;
		t2.CurrentPlayer = otherPlayer;
		Assert.IsFalse(t1.HaveNeighborEnemy());
	}

	[Test]
	public void TroopsCount(){
		var t1 = Substitute.For<Territory>();
		t1.Awake();
		var troop = Substitute.For<Troop>();
		t1.AddTroop(troop);
		Assert.AreEqual(t1.TroopsCount,1);
	}

	[Test]
	public void TroopsCountAdding(){
		var t1 = Substitute.For<Territory>();
		t1.Awake();
		var troop = Substitute.For<Troop>();
		var troop2 = Substitute.For<Troop>();
		t1.AddTroop(troop);
		t1.AddTroop(troop2);
		Assert.AreNotEqual(t1.TroopsCount,1);
	}

	[Test]
	public void TerritoryAdderByPlayer(){
		var player = Substitute.For<Player>();
		var t1 = Substitute.For<Territory>();
		t1.CurrentPlayer = player;
		Assert.IsTrue(player.HaveTerritory(t1));
	}

}