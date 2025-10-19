package bt_java.a6singleton;

import bt_java.creational.singleton.vote.Election;

import java.util.ArrayList;
import java.util.List;

public class DataAccess {
    private static DataAccess instance;
    List<SanPham> dsSP;

    private DataAccess(){
        dsSP = new ArrayList<>();
    }

    public static DataAccess getInstance(){
        if(instance == null)
            instance = new DataAccess();
        return instance;
    }

    public void them(SanPham s){
        dsSP.add(s);
    }

    public void inKQ(){
        for(var s: dsSP)
            System.out.println(s.toString());
    }
}
