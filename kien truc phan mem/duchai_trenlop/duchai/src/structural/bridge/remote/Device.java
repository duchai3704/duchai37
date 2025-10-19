package structural.bridge.remote;

public interface Device {
    boolean isEnable();

    void enable();

    void disable();

    int getVolume();

    void setVolume(int var1);
}
