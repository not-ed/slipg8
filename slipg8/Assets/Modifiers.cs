using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Modifiers : MonoBehaviour {

	public enum MODIFIER
    {
        DISABLED,REVERSED_CONTROLS,CLOUDED_VIEW,FLIPPED_SCREEN,DISABLE_JUMP,HIDDEN_HAZARDS,INVISIBLE_MOVE, FLOAT_STOP, SCREEN_LAG
    }

    public MODIFIER[] current_modifiers = new MODIFIER[3];
    [SerializeField]
    private Image[] MOD_ICO = new Image[3];
    [SerializeField]
    private Sprite ICO_DISABLED,ICO_REVERSED_CONTROLS, ICO_CLOUDED_VIEW, ICO_FLIPPED_SCREEN, ICO_DISABLE_JUMP, ICO_HIDDEN_HAZARDS, ICO_INVISIMOVE, ICO_FLOAT_STOP, ICO_SCREEN_LAG;


    public bool HasModifier(MODIFIER mod)
    {
        if (current_modifiers[0] == mod || current_modifiers[1] == mod || current_modifiers[2] == mod)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpdateModifierIcons()
    {
        for (int i = 0; i < 3; i++)
        {
            switch (current_modifiers[i])
            {
                case MODIFIER.DISABLED:
                    MOD_ICO[i].sprite = ICO_DISABLED;
                    break;
                case MODIFIER.REVERSED_CONTROLS:
                    MOD_ICO[i].sprite = ICO_REVERSED_CONTROLS;
                    break;
                case MODIFIER.CLOUDED_VIEW:
                    MOD_ICO[i].sprite = ICO_CLOUDED_VIEW;
                    break;
                case MODIFIER.FLIPPED_SCREEN:
                    MOD_ICO[i].sprite = ICO_FLIPPED_SCREEN;
                    break;
                case MODIFIER.DISABLE_JUMP:
                    MOD_ICO[i].sprite = ICO_DISABLE_JUMP;
                    break;
                case MODIFIER.HIDDEN_HAZARDS:
                    MOD_ICO[i].sprite = ICO_HIDDEN_HAZARDS;
                    break;
                case MODIFIER.INVISIBLE_MOVE:
                    MOD_ICO[i].sprite = ICO_INVISIMOVE;
                    break;
                case MODIFIER.FLOAT_STOP:
                    MOD_ICO[i].sprite = ICO_FLOAT_STOP;
                    break;
                case MODIFIER.SCREEN_LAG:
                    MOD_ICO[i].sprite = ICO_SCREEN_LAG;
                    break;
                default:
                    break;
            }
        }
    }

    public void ReshuffleModifiers(int modifier_count)
    {
        MODIFIER[] last_modifier = new MODIFIER[3];
        last_modifier[0] = current_modifiers[0];
        last_modifier[1] = current_modifiers[1];
        last_modifier[2] = current_modifiers[2];
        if (modifier_count > 0)
        {
            do
            {
                current_modifiers[0] = (MODIFIER)Random.Range(1, 9);
            } while (current_modifiers[0] == last_modifier[0]);
        }
        else
        {
            current_modifiers[0] = MODIFIER.DISABLED;
        }


        if (modifier_count > 1)
        {
            do
            {
                current_modifiers[1] = (MODIFIER)Random.Range(1, 9);
            } while (current_modifiers[1] == current_modifiers[0] || current_modifiers[1] == MODIFIER.DISABLED || current_modifiers[1] == last_modifier[1]);
        }
        else
        {
            current_modifiers[1] = MODIFIER.DISABLED;
        }

        if (modifier_count > 2)
        {
            do
            {
                current_modifiers[2] = (MODIFIER)Random.Range(1, 9);
            } while (current_modifiers[2] == current_modifiers[0] || current_modifiers[2] == current_modifiers[1] || current_modifiers[2] == MODIFIER.DISABLED || current_modifiers[2] == last_modifier[2]);
        }
        else
        {
            current_modifiers[2] = MODIFIER.DISABLED;
        }

        UpdateModifierIcons();
    }

}
