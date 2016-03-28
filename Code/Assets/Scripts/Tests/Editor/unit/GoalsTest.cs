//using System;
//using System.Collections.Generic;
//using System.Threading;
//using NUnit.Framework;
//using UnityEngine;
//using NSubstitute;

//[TestFixture]
//[Category("Goals Test")]
//internal class GoalsTest{ 

//	public GameController MockGameController(){
//		return Substitute.For<GameController>();
//	}

//	public Player MockPlayer(){
//		return Substitute.For<Player>();
//	}

//	public Player MockPlayer(List<Territory> territories){
//		var p = Substitute.For<Player>();
//		foreach(Territory t in territories){
//			p.AddTerritory(t);
//		}
//		return p;
//	}

//	public List<Territory> MockTerritories(int count){
//		List<Territory> terr = new List<Territory>();
//		for(int i =0; i < count; i++){
//			var t = Substitute.For<Territory>();
//			terr.Add(t);
//		}
//		return terr;
//	}

//	public List<Continent> MockContinents(int count){
//		List<Continent> continents = new List<Continent>();
//		for(int i =0; i < count; i++){
//			var c = Substitute.For<Continent>();
//			continents.Add(c);
//		}
//		return continents;
//	}

//	[Test]
//	public void TerritoriesGoalPlayerHaveNotTerritories(){
//		GameController game = MockGameController();
//		var territories = MockTerritories(10);
//		var others = MockTerritories(9);
//		var player = MockPlayer();
//		Goal goal = new TerritoriesGoal(territories.ToArray());
//		Assert.IsFalse(goal.Check(game,player));
//	}

//	[Test]
//	public void TerritoriesGoalPlayerHaveTerritories(){
//		GameController game = MockGameController();
//		var territories = MockTerritories(1);
//		var player = Substitute.For<Player>();
//		player.AddTerritory(territories[0]);
//		Goal goal = new TerritoriesGoal(territories.ToArray());
//		Assert.IsTrue(goal.Check(game,player));
//	}

//	[Test]
//	public void TerritoriesGoalPlayerHaveNotTerritoriesCount(){
//		GameController game = MockGameController();
//		var territories = MockTerritories(1);
//		var player = Substitute.For<Player>();
//		player.AddTerritory(territories[0]);
//		Goal goal = new TerritoriesGoal(2);
//		Assert.IsFalse(goal.Check(game,player));
//	}

//	[Test]
//	public void TerritoriesGoalPlayerHaveTerritoriesCount(){
//		GameController game = MockGameController();
//		var territories = MockTerritories(1);
//		var player = Substitute.For<Player>();
//		player.AddTerritory(territories[0]);
//		Goal goal = new TerritoriesGoal(1);
//		Assert.IsTrue(goal.Check(game,player));
//	}

//	[Test]
//	public void PlayerDefeatGoalTrue(){
//		GameController game = MockGameController();
//		Player player = Substitute.For<Player>();
//		Player otherPlayer = Substitute.For<Player>();
//		Goal goal = new DestroyPlayerGoal(player);
//		Assert.IsTrue(goal.Check(game,otherPlayer));
//	}

//	[Test]
//	public void PlayerDefeatGoalFalse(){
//		GameController game = MockGameController();
//		Player player = Substitute.For<Player>();
//		var territories = MockTerritories(1);
//		player.AddTerritory(territories[0]);
//		Player otherPlayer = Substitute.For<Player>();
//		Goal goal = new DestroyPlayerGoal(player);
//		Assert.IsFalse(goal.Check(game,otherPlayer));
//	}

//	[Test]
//	public void PlayerHaveContinentsGoal(){
//		GameController game = MockGameController();
//		Map m = Substitute.For<Map>();
//		game.currentMap = m;
//		var territories = MockTerritories(10);
//		var otherTerritories = MockTerritories(9);
//		Player player = MockPlayer(otherTerritories);
//		var continents = MockContinents(1);
//		var otherContinents = MockContinents(1);
//		otherContinents[0].territories = otherTerritories.ToArray();
//		continents[0].territories = territories.ToArray();
//		m.continents = otherContinents.ToArray();
//		Goal goal = new ContinentsGoal(otherContinents.ToArray());
//		Assert.IsTrue(goal.Check(game,player));
//	}

//	[Test]
//	public void PlayerHaveNotContinentsCountGoal(){
//		GameController game = MockGameController();
//		Map m = Substitute.For<Map>();
//		game.currentMap = m;
//		var territories = MockTerritories(10);
//		var otherTerritories = MockTerritories(9);
//		Player player = MockPlayer(otherTerritories);
//		var continents = MockContinents(1);
//		var otherContinents = MockContinents(1);
//		otherContinents[0].territories = otherTerritories.ToArray();
//		continents[0].territories = territories.ToArray();
//		m.continents = otherContinents.ToArray();
//		Goal goal = new ContinentsGoal(2);
//		Assert.IsFalse(goal.Check(game,player));
//	}

//}