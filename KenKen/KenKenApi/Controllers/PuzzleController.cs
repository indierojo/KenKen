using System;
using System.Collections.Generic;
using System.Web.Http;
using Domain;
using KenKenBuilder;

namespace KenKenApi.Controllers
{
    public class PuzzleController : ApiController
    {
        // GET api/puzzle
        public IEnumerable<Puzzle> Get()
        {
            throw new NotSupportedException();
        }

        // GET api/puzzle/5
        public Puzzle Get(int id)
        {
            return new SimpleKenKenPuzzleBuilder().Build(DifficultyLevel.Easy, GridSize.FourByFour);
        }

        // POST api/puzzle
        public void Post([FromBody]Puzzle value)
        {
            throw new NotSupportedException();
        }

        // PUT api/puzzle/5
        public void Put(int id, [FromBody]Puzzle value)
        {
            throw new NotSupportedException();
        }

        // DELETE api/puzzle/5
        public void Delete(int id)
        {
            throw new NotSupportedException();
        }
    }
}