using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPopupGunManage : UIPopup
{
    #region Fields

    private Button closedBtn;
    private Button equipBtn;
    private List<Toggle> GunToggles;
    private List<Toggle> accessoryToggles;

    #endregion

    #region Initialize

    protected override void Init()
    {
        base.Init();
        SetButtons();
        SetEvents();
        SetToggles();
        ChooseToggles();
    }

    private void SetToggles()
    {
        SetUI<Toggle>();
        Toggle tempval;

        GunToggles = new List<Toggle>();
        accessoryToggles = new List<Toggle>();

        tempval = GetUI<Toggle>("Toggle_Weapon_HG");
        GunToggles.Add(tempval);
        tempval = GetUI<Toggle>("Toggle_Weapon_SR");
        GunToggles.Add(tempval);
        tempval = GetUI<Toggle>("Toggle_Weapon_SG");
        GunToggles.Add(tempval);
        tempval = GetUI<Toggle>("Toggle_Weapon_MG");
        GunToggles.Add(tempval);

        tempval = GetUI<Toggle>("Toggle_Parts_Sight");
        accessoryToggles.Add(tempval);
        tempval = GetUI<Toggle>("Toggle_Parts_Muzzle");
        accessoryToggles.Add(tempval);
        tempval = GetUI<Toggle>("Toggle_Parts_Grip");
        accessoryToggles.Add(tempval);
        tempval = GetUI<Toggle>("Toggle_Parts_Magazine");
        accessoryToggles.Add(tempval);
    }

    private void SetButtons()
    {
        SetUI<Button>();
        closedBtn = GetUI<Button>("Btn_Closed");
        equipBtn = GetUI<Button>("Button_Equip");
    }

    private void SetEvents()
    {
        closedBtn.gameObject.SetEvent(UIEventType.Click, CloseGunManagePopup);
        equipBtn.gameObject.SetEvent(UIEventType.Click, EquipWeapon);
    }

    #endregion

    private void CloseGunManagePopup(PointerEventData eventData)
    {
        UI.ClosePopup(this);
    }

    private void ChooseToggles()
    {
        int gunType = -1;
        for(int i=0; i < GunToggles.Count; i++)
        {
            ItemData itemData = GunToggles[i].GetComponent<UIGunItem>().GunData;
            if(itemData == WeaponEquipManager.Instance.CurrentWeapon.Data)
            {
                GunToggles[i].isOn = true;
                gunType = i;
                break;
            }
        }

        AccModifier accMod = new AccModifier(); // Defalut Value
        AccModifier curMod = WeaponEquipManager.Instance.CurrentWeapon.AccModifier;

        // Sight, Muzzle, Grip, Magazine
        if(!(accMod.AimModifier == curMod.AimModifier))
        {
            accessoryToggles[0].isOn = true;
        }

        if(!(accMod.SoundModifier == curMod.SoundModifier))
        {
            accessoryToggles[1].isOn = true;
        }

        if(!(accMod.RecoilModifier == curMod.RecoilModifier))
        {
            accessoryToggles[2].isOn = true;
        }

        if(!(accMod.MagazineModifier == curMod.MagazineModifier))
        {
            accessoryToggles[3].isOn = true;
        }

        if(gunType == 1)
        {
            if(curMod.AimModifier == 0.7f) // 저격총 기본 배율
            {
                accessoryToggles[0].isOn = false;
            }
        }
    }

    private void EquipWeapon(PointerEventData eventData)
    {
        if (MainScene.Instance.Player.IsADS)
            MainScene.Instance.Player.ChangeADS();

        int gunType = -1;
        for(int i=0; i < GunToggles.Count; i++)
        {
            if(GunToggles[i].isOn == true)
            {
                gunType = i;
                break;
            }
        }
        AccModifier accMod = new AccModifier();

        if(gunType == 1)
        {
            accMod.ChangeAim(0.7f);
        }

        for(int i=0; i < accessoryToggles.Count; i++)
        {
            if(accessoryToggles[i].isOn == true)
            {
                // Sight, Muzzle, Grip, Magazine
                switch (i)
                {
                    case 0:
                    if(gunType == 1)
                    {
                        accMod.ChangeAim(0.3f);
                    }
                    else
                    {
                        accMod.ChangeAim(0.5f);
                    }
                    break;
                    case 1:
                    {
                        accMod.ChangeSound(0.3f);
                    }
                    break;
                    case 2:
                    {
                        accMod.ChangeRecoil(0.5f);
                    }
                    break;
                    case 3:
                    {
                        if(gunType == 3)
                        {
                            accMod.ChangeMagazine(30);
                        }
                        else
                        {
                            accMod.ChangeMagazine(5);
                        }
                    }
                    break;
                }
            }
        }

        WeaponEquipManager.Instance.SetWeapon(gunType, accMod);


        UI.ClosePopup(this);
    }
}
