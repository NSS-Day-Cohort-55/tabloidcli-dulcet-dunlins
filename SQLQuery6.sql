SELECT b.Id AS BlogId,
b.Title,
b.Url,
t.Id AS TagId,
t.Name
FROM Blog b
LEFT JOIN BlogTag bt on b.Id = bt.BlogId
LEFT JOIN Tag t on t.Id = bt.TagId
WHERE b.id = @id;

SELECT a.Id AS AuthorId,
a.FirstName,
a.LastName,
a.Bio,
t.Id AS TagId,
t.Name
FROM Author a 
LEFT JOIN AuthorTag at on a.Id = at.AuthorId
LEFT JOIN Tag t on t.Id = at.TagId
WHERE a.id = @id