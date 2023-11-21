namespace Kariyer.Business.Services;

public interface HarmfulWordsService {

	Task<List<string>> List(string word = "");

	Task Create(string word);

	Task Update(string oldWord, string newWord);

	Task Delete(string word);

	Task<bool> Contains(string description);
}
