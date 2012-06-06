/*
 *  Copyright (C) 2012 k_os <ben.at.hemio.de>
 *  Copyright (C) 2012 Ben Tucker <ben.at.tucker.org>
 * 
 *  This file is part of rndWalker.
 *
 *  rndWalker is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  rndWalker is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with Foobar.  If not, see <http://www.gnu.org/licenses/>. 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using rndWalker.Common;
using D3;

namespace rndWalker.Classes
{
    public static class Wizard
    {
        public static Spell diamondSkin = new Spell(SNOPowerId.Wizard_DiamondSkin, 6, 15, 0, 0);
        public static Spell hydra = new Spell(SNOPowerId.Wizard_Hydra, 15, 0, 15, 0);
        public static Spell energyArmor = new Spell(SNOPowerId.Wizard_EnergyArmor, 120, 0, 25, 0);
        public static Spell magicWeapon = new Spell(SNOPowerId.Wizard_MagicWeapon, 300, 0, 25, 0);
        public static Spell blizzard = new Spell(SNOPowerId.Wizard_Blizzard, 6, 0, 45, 0);
        public static Spell magicMissile = new Spell(SNOPowerId.Wizard_MagicMissile, 0, 0, 0, 0);

        public static void drinkPot() {
            // hotfix as this does not work via power
            //1FB50094: E1F43DD874E42728 Root.NormalLayer.game_dialog_backgroundScreenPC.game_potion (Visible: True)
            UIElement.Get(0xE1F43DD874E42728).Click();
        }

        public static bool AttackUnit(Unit _unit, TimeSpan _timeout) {
            if (_unit.Life <= 0) {
                return false;
            }

            TimeSpan startTime = TimeSpan.FromTicks(System.Environment.TickCount);

            while (_unit.Life > 0) {
                blizzard.use(Me.X, Me.Y);
                hydra.use(Me.X, Me.Y);
                magicMissile.use(Attack.GetClosest());

                if (1.0 * Me.Life / Me.MaxLife < 0.75) {
                    diamondSkin.use();
                }

                if (1.0 * Me.Life / Me.MaxLife < 0.3) {
                    drinkPot();
                }

                Thread.Sleep(200);

                if (TimeSpan.FromTicks(System.Environment.TickCount).Subtract(startTime) > _timeout) {
                    return false;
                }
            }

            return true;
        }
    }
}
