import {List, ListItem, ListItemText, Typography, Paper, Avatar, Box, Collapse} from '@mui/material';
import {DateFormat} from "../utils/DateFormat";
import ExpandableText from "./ExpandableText";
import {useState} from "react";
import {ExpandLess, ExpandMore} from "@mui/icons-material";

const CommentList = ({ comments }) => {
    const [open, setOpen] = useState(false);
    const handleToggle = () => {
        setOpen(!open);
    };
    return (
        <>
            {comments && comments.length > 0 ? (
                <>
                    <List component="nav">
                        <ListItem onClick={handleToggle}>
                            <ListItemText primary={open ? "Zwiń komentarze" : `Pokaż ${comments.length} komentarze`}/>
                            {open ? <ExpandLess /> : <ExpandMore />}
                        </ListItem>
                    </List>
                    <Collapse in={open} timeout="auto" unmountOnExit>
                        <List component="div" disablePadding sx={{mb:10}}>
                            {comments.map((comment) => (
                                <ListItem key={comment.id}>
                                    <Paper elevation={3} style={{ padding: '10px', width: '100%' }}>
                                        <Box display="flex" alignItems="center" justifyContent="space-between">
                                            <Box display="flex" alignItems="center" flexDirection="column">
                                                <Avatar />
                                                <Box marginTop="10px">
                                                    {comment.userName ? (
                                                        <Typography variant="subtitle1">{comment.userName}</Typography>
                                                    ) : (
                                                        <Typography variant="subtitle1">Użytkownik usunięty</Typography>
                                                    )}
                                                </Box>
                                            </Box>
                                            <Box flex="1" m={5}>
                                                <ExpandableText text={comment.content} maxLength={100} />
                                            </Box>
                                            <Box>
                                                <Typography variant="caption">{DateFormat(comment.data)}</Typography>
                                            </Box>
                                        </Box>
                                    </Paper>
                                </ListItem>
                            ))}
                        </List>
                    </Collapse>
                </>
            ) : (
                <Typography variant="body1" m={4}>Brak komentarzy</Typography>
            )}
        </>
    );
};
export default CommentList;
