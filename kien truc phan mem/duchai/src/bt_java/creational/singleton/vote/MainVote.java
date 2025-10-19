package bt_java.creational.singleton.vote;

public class MainVote {
    public static void main(String[] args) {
        User hai = new User("Hai");
        User kiet = new User("Kiet");
        User bao = new User("Bao");
        hai.vote(Candidate.TRUMP);
        kiet.vote(Candidate.BIDEN);
        bao.vote(Candidate.TRUMP);
        Election.getInstance().printResult();
        bao.vote(Candidate.TRUMP);
        Election.getInstance().printResult();
    }
}
