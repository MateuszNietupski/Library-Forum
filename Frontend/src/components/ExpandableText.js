import {useState} from "react";
import {Typography,Button} from "@mui/material";

const ExpandableText = ({text, maxLength}) => {
  const [isExpanded,setIsExpanded] = useState(false);
  const toggleExpansion = () => {
      setIsExpanded(!isExpanded);
  }
  const displayText = !isExpanded ? text : `${text.substring(0,maxLength)}...` 
  return (
    <div >
        <Typography variant="body1" textAlign="center">{displayText}</Typography>
        {text.length > maxLength && (
                <Button onClick={toggleExpansion} alignItems="center">
                    {!isExpanded ? 'Zwiń' : 'Rozwiń'}
                </Button>
        )}
    </div>  
  );
}
export default ExpandableText;