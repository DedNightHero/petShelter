using System;

namespace MainForm.BLogic
{
    public class BLogic : IBL
    {
        private Accessors.AnimalsAccessor aa;
        private Accessors.DebitCreditAccessor dca;
        private Accessors.GoodsAccessor ga;
        private Accessors.GoodsTypeAccessor gta;
        private Accessors.PositionsAccessor pa;
        private Accessors.SpeciesAccessor sa;
        private Accessors.UsersAccessor ua;
        private AbstractConnection c;
        private AbstractTransaction t;
        private PetShelter ps1;

        public BLogic()
        {
            CreateAccessors();
        }

        private void CreateAccessors()
        {
            aa = new Accessors.AnimalsAccessor();
            dca = new Accessors.DebitCreditAccessor();
            ga = new Accessors.GoodsAccessor();
            gta = new Accessors.GoodsTypeAccessor();
            pa = new Accessors.PositionsAccessor();
            sa = new Accessors.SpeciesAccessor();
            ua = new Accessors.UsersAccessor();
        }

        public PetShelter getAnimals()
        {
            c = ConnectionFactory.CreateConnection();
            c.Open();
            ps1 = new PetShelter();
            try
            {
                t = c.BeginTransaction();
                try
                {
                    aa.ReadData(t, c, ps1);
                    t.Commit();
                }
                catch (Exception e)
                {
                    t.Rollback();
                    throw e;
                }
            }
            finally
            {
                c.Close();
            }
            return ps1;
        }

        public void setAnimals(PetShelter ps1)
        {
            c = ConnectionFactory.CreateConnection();
            c.Open();
            try
            {
                t = c.BeginTransaction();
                try
                {
                    aa.WriteData(t, c, ps1);
                    t.Commit();
                }
                catch (Exception e)
                {
                    t.Rollback();
                    throw e;
                }
            }
            finally
            {
                c.Close();
            }
        }

        public PetShelter getDebitCredit()
        {
            c = ConnectionFactory.CreateConnection();
            c.Open();
            ps1 = new PetShelter();
            try
            {
                t = c.BeginTransaction();
                try
                {
                    dca.ReadData(t, c, ps1);
                    t.Commit();
                }
                catch (Exception e)
                {
                    t.Rollback();
                    throw e;
                }
            }
            finally
            {
                c.Close();
            }
            return ps1;
        }

        public void setDebitCredit(PetShelter ps1)
        {
            c = ConnectionFactory.CreateConnection();
            c.Open();
            try
            {
                t = c.BeginTransaction();
                try
                {
                    dca.WriteData(t, c, ps1);
                    t.Commit();
                }
                catch (Exception e)
                {
                    t.Rollback();
                    throw e;
                }
            }
            finally
            {
                c.Close();
            }
        }

        public PetShelter getGoods()
        {
            c = ConnectionFactory.CreateConnection();
            c.Open();
            ps1 = new PetShelter();
            try
            {
                t = c.BeginTransaction();
                try
                {
                    ga.ReadData(t, c, ps1);
                    t.Commit();
                }
                catch (Exception e)
                {
                    t.Rollback();
                    throw e;
                }
            }
            finally
            {
                c.Close();
            }
            return ps1;
        }

        public void setGoods(PetShelter ps1)
        {
            c = ConnectionFactory.CreateConnection();
            c.Open();
            try
            {
                t = c.BeginTransaction();
                try
                {
                    ga.WriteData(t, c, ps1);
                    t.Commit();
                }
                catch (Exception e)
                {
                    t.Rollback();
                    throw e;
                }
            }
            finally
            {
                c.Close();
            }
        }

        public PetShelter getGoodsType()
        {
            c = ConnectionFactory.CreateConnection();
            c.Open();
            ps1 = new PetShelter();
            try
            {
                t = c.BeginTransaction();
                try
                {
                    gta.ReadData(t, c, ps1);
                    t.Commit();
                }
                catch (Exception e)
                {
                    t.Rollback();
                    throw e;
                }
            }
            finally
            {
                c.Close();
            }
            return ps1;
        }

        public void setGoodsType(PetShelter ps1)
        {
            c = ConnectionFactory.CreateConnection();
            c.Open();
            try
            {
                t = c.BeginTransaction();
                try
                {
                    gta.WriteData(t, c, ps1);
                    t.Commit();
                }
                catch (Exception e)
                {
                    t.Rollback();
                    throw e;
                }
            }
            finally
            {
                c.Close();
            }
        }

        public PetShelter getPositions()
        {
            c = ConnectionFactory.CreateConnection();
            c.Open();
            ps1 = new PetShelter();
            try
            {
                t = c.BeginTransaction();
                try
                {
                    pa.ReadData(t, c, ps1);
                    t.Commit();
                }
                catch (Exception e)
                {
                    t.Rollback();
                    throw e;
                }
            }
            finally
            {
                c.Close();
            }
            return ps1;
        }

        public void setPositions(PetShelter ps1)
        {
            c = ConnectionFactory.CreateConnection();
            c.Open();
            try
            {
                t = c.BeginTransaction();
                try
                {
                    pa.WriteData(t, c, ps1);
                    t.Commit();
                }
                catch (Exception e)
                {
                    t.Rollback();
                    throw e;
                }
            }
            finally
            {
                c.Close();
            }
        }

        public PetShelter getSpecies()
        {
            c = ConnectionFactory.CreateConnection();
            c.Open();
            ps1 = new PetShelter();
            try
            {
                t = c.BeginTransaction();
                try
                {
                    sa.ReadData(t, c, ps1);
                    t.Commit();
                }
                catch (Exception e)
                {
                    t.Rollback();
                    throw e;
                }
            }
            finally
            {
                c.Close();
            }
            return ps1;
        }

        public void setSpecies(PetShelter ps1)
        {
            c = ConnectionFactory.CreateConnection();
            c.Open();
            try
            {
                t = c.BeginTransaction();
                try
                {
                    sa.WriteData(t, c, ps1);
                    t.Commit();
                }
                catch (Exception e)
                {
                    t.Rollback();
                    throw e;
                }
            }
            finally
            {
                c.Close();
            }
        }

        public PetShelter getUsers()
        {
            c = ConnectionFactory.CreateConnection();
            c.Open();
            ps1 = new PetShelter();
            try
            {
                t = c.BeginTransaction();
                try
                {
                    ua.ReadData(t, c, ps1);
                    t.Commit();
                }
                catch (Exception e)
                {
                    t.Rollback();
                    throw e;
                }
            }
            finally
            {
                c.Close();
            }
            return ps1;
        }

        public void setUsers(PetShelter ps1)
        {
            c = ConnectionFactory.CreateConnection();
            c.Open();
            try
            {
                t = c.BeginTransaction();
                try
                {
                    ua.WriteData(t, c, ps1);
                    t.Commit();
                }
                catch (Exception e)
                {
                    t.Rollback();
                    throw e;
                }
            }
            finally
            {
                c.Close();
            }
        }
    }
}