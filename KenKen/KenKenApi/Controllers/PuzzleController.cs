using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Http;
using System.Web.Http.Cors;
using Domain;
using KenKenBuilder;
using PuzzleValidator;

namespace KenKenApi.Controllers
{
    [EnableCors(origins: "http://localhost:59519", headers: "*", methods: "*")]
    public class PuzzleController : ApiController
    {
        private static readonly int SmallTestPuzzleId = -124;

        // GET api/puzzle
        public IEnumerable<Puzzle> Get()
        {
            throw new NotImplementedException();
        }

        [Route("api/puzzle/random/{puzzleSize}/")]
        [HttpGet]
        public Puzzle Random(int puzzleSize)
        {
            if (puzzleSize < 3 || puzzleSize > 9)
            {
                throw new ArgumentOutOfRangeException("puzzleSize", "Puzzle size must be within 3 and 9!");
            }
            return new KenKenPuzzleBuilder().Build(DifficultyLevel.Easy, (GridSize)puzzleSize);
        }

        // GET api/puzzle/5
        public Puzzle Get(int id)
        {
            if (id == SmallTestPuzzleId)
            {
                return TinyTestPuzzleBuilder.GetTinyPuzzle();
            }

            return new SimpleKenKenPuzzleBuilder().Build(DifficultyLevel.Easy, GridSize.FourByFour);
        }

        // POST api/puzzle/check
        [Route("api/puzzle/check")]
        [HttpPost]
        public ValidationResult Check([FromBody]Puzzle submittedAnswer)
        {
            if (submittedAnswer == null)
            {
                throw new ArgumentException("Could not parse values into expected format.");
            }

            return PuzzleValidationService.CheckForValidity(submittedAnswer);
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