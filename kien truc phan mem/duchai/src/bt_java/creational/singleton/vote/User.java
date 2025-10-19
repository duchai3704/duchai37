package bt_java.creational.singleton.vote;

public class User {
    String name;
    public User(String name){
        this.name = name;
    }


    public void vote(Candidate c){
        Election.getInstance().vote(c, this.name);
    }
}
