package state.remote;

public class OffState implements IRemoteState {
    @Override
    public void handle(RemoteControl remote) {
        System.out.println("Bật thiết bị...");
        remote.setState(new OnState()); // Chuyển về trạng thái On
    }
}
