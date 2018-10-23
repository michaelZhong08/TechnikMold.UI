/*
 * Create By:lechun1
 * 
 * Description: 
 * 1. Define the naming convension for required object types
 *      The naming convension has following rules:
 *      1. Name is combined by upper case letters and numbers
 *      2. Upper case letters will not be changed when generating a new number
 *      3. Zero(0) represents a number which will be changed when generating a new number.
 *      4. Non-zero will not be modified 
 * 2. To generate the sequence of different object types
 * 
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class SequenceRepository:ISequenceRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<Sequence> Sequences
        {
            get {
                return _context.Sequences;
            }
        }

        public int Save(Sequence Sequence)
        {
            if (Sequence.SequenceID == 0)
            {
                _context.Sequences.Add(Sequence);
            }
            else
            {
                Sequence _dbEntry = _context.Sequences.Find(Sequence.SequenceID);
                if (_dbEntry != null)
                {
                    _dbEntry.Name = Sequence.Name;
                    _dbEntry.NameConvension = Sequence.NameConvension;
                    _dbEntry.Current = Sequence.Current;
                }
            }
            _context.SaveChanges();
            return Sequence.SequenceID;
        }

        /// <summary>
        /// Generate a new number of specified object type
        /// </summary>
        /// <param name="Type">Name of the type</param>
        /// <returns></returns>
        public string GetNextNumber(string Type)
        {
            Sequence _sequence = _context.Sequences.Where(s => s.Name.ToLower() == Type.ToLower()).FirstOrDefault();
            string _result = _sequence.NameConvension;
            int  _nextNumber = _sequence.Current+1;

            _result = _result.Substring(0, _result.Length - _nextNumber.ToString().Length) + _nextNumber;
            Increase(_sequence.SequenceID);
            #region YYMM 替换
            string _yymm = DateTime.Now.ToString("yyMM");
            _result=_result.Replace("YYMM", _yymm);
            #endregion
            return _result;
        }

        private void Increase(int SequenceID)
        {
            Sequence _sequence = _context.Sequences.Find(SequenceID);
            _sequence.Current = _sequence.Current + 1;
            _context.SaveChanges();
        }
    }
}
