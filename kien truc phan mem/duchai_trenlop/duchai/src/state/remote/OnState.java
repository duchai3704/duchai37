package state.remote;

public class OnState implements IRemoteState{
    @Override
    public void handle(RemoteControl remote) {
        System.out.println("Tắt thiết bị...");
        remote.setState(new OffState()); // Chuyển về trạng thái Off
    }
}
