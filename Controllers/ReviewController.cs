using finalyearproject.Models;
using finalyearproject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace finalyearproject.Controllers
{
    public class ReviewController : Controller
    {
        private ApplicationDBcontext _dbContext;
        private ReviewRepo _reviewRepo;
        public ReviewController(ApplicationDBcontext dbContext, ReviewRepo reviewRepo)
        {
            _dbContext = dbContext;
            _reviewRepo = new ReviewRepo(_dbContext);
        }
        public async Task<IActionResult> Index()
        {
            List<Review> reviews = await _reviewRepo.GetAllReviews();
            return View(reviews);
        }
        public async Task<IActionResult> Details(int reviewId)
        {
            Review review= await _reviewRepo.GetReviewById(reviewId);

            return View(review);
        }
        public void HandleReviewConpany(Review review)
        {
            _dbContext.Add(review);
            _dbContext.SaveChanges();
        }
        public async Task<IActionResult> UpdateReview(int reviewId)
        {
            Review review = await _reviewRepo.GetReviewById(reviewId);
            return View(review);
        }
        [HttpPut]
        public IActionResult UpdateReview(Review review)
        {
            HandleUpdateReview(review);
            return RedirectToAction("Index");
        }
        private void HandleUpdateReview(Review review)
        {
            _dbContext.Update(review);
            _dbContext.SaveChanges();
        }
        public async void HandleDeleteReview(int reviewId)
        {
            Review review= await _reviewRepo.GetReviewById(reviewId);
            _dbContext.Remove(review);
            _dbContext.SaveChanges();
        }
    }
}
