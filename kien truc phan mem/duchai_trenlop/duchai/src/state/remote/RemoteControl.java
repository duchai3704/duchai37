package state.remote;

public class RemoteControl{
    private IRemoteState state;

    public RemoteControl() {
        state = new OffState();
    }

    public void setState(IRemoteState state) {
        this.state = state;
    }

    public void powerPress() {
        state.handle(this); // Gọi phương thức xử lý của trạng thái hiện tại
    }
}
