namespace HMS.LogicProvider;

public record GETAnnouncementsDTO(string id, 
                                  string title,
                                  string content, 
                                  string fileUrl,
                                  DateTime createdDate,
                                  bool isDeleted);
