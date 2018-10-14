using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainForm.BLogic
{
    public interface IBL
    {
        PetShelter getAnimals();
        PetShelter getDebitCredit();
        PetShelter getGoods();
        PetShelter getGoodsType();
        PetShelter getPositions();
        PetShelter getSpecies();
        PetShelter getUsers();

        void setAnimals(PetShelter ps1);
        void setDebitCredit(PetShelter ps1);
        void setGoods(PetShelter ps1);
        void setGoodsType(PetShelter ps1);
        void setPositions(PetShelter ps1);
        void setSpecies(PetShelter ps1);
        void setUsers(PetShelter ps1);
    }
}
