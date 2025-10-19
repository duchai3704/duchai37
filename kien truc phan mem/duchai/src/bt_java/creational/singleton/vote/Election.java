package bt_java.creational.singleton.vote;

import java.util.ArrayList;
import java.util.List;

public class Election {
    private static Election instance;
    private int trump, biden;
    List<String> userID;
    private Election(){
        trump = 0;
        biden = 0;
        userID = new ArrayList<>();
    }
    public static Election getInstance(){
        if(instance == null)
            instance = new Election();
        return instance;
    }

    protected void vote(Candidate c, String userID){
        for(var id:userID)
            if(id.equals(userID))
                return;
        userID.add(userID);

        if(c == Candidate.TRUMP)
            trump++;
        else if( c == Candidate.BIDEN)
            biden++;
    }

    public void printResult(){
        System.out.println("Trump: "+ this.trump);
        System.out.println("Biden: "+ this.biden);
    }
}
