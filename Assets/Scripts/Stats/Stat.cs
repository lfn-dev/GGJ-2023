using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private int maxValue;
    [SerializeField] private bool hasMaxValue = true;
    [SerializeField] private int minValue = 1;
    [SerializeField] private bool hasMinValue = true;
    [SerializeField] private int defaultValue;

    private int value;
    private bool initialized = false;

    public int GetMax(){
        return maxValue;
    }
    public int GetMin(){
        return minValue;
    }

    public int GetValue(){
        if(!initialized){
            return ResetValue();
        }
        return value;
    }

    public int SetValue(int num){
        if(hasMinValue && (num < minValue)){
            num = minValue;
        }
        else if(hasMaxValue && (num > maxValue)){
            num = maxValue;
        }
        value = num;
        initialized = true;
        
        return value;
    }

    public int AddValue(int num){
        return SetValue(GetValue() + num);
    }

    public int ResetValue(){
        return SetValue(defaultValue);
    }
}
