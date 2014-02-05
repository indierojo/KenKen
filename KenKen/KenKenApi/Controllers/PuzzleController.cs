using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Http;
using Domain;
using KenKenBuilder;
using PuzzleValidator;

namespace KenKenApi.Controllers
{
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
            if (puzzleSize < 3 || puzzleSize > 7)
            {
                throw new ArgumentOutOfRangeException("puzzleSize", "Puzzle size must be within 3 and 7!");
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

        // POST api/puzzle/5/check
        [Route("api/puzzle/{puzzleId}/check")]
        [HttpPost]
        public ValidationResult Check(int puzzleId, [FromBody]Cell[][] submittedAnswer)
        {
            if (submittedAnswer == null)
            {
                throw new ArgumentException("Could not parse values into expected format.");
            }

            var puzzleDefinition = Get(puzzleId);
            var puzzleToCheck = new Puzzle(submittedAnswer, puzzleDefinition.Groups);

            return PuzzleValidationService.CheckForValidity(puzzleToCheck);
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