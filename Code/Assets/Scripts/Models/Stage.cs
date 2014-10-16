

public enum Stage{ ALLOCK, ATTACK, MOVE, END}

public class StageUtils{
	public static Stage NextStage(Stage stage){
		return stage + 1;
	}
}
