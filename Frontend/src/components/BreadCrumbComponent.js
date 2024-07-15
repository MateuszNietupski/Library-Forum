import {Breadcrumbs,Typography,Link} from "@mui/material";

const BreadCrumbsComponent = ({breadcrumbs,primaryName}) => {
    return (
        <Breadcrumbs sx={{m:4}} aria-label="breadcrumb">
            {breadcrumbs && breadcrumbs.length > 0 &&
            breadcrumbs.map((breadcrumb) => (
                <Link key={breadcrumb.url} underline="hover" color="inherit" href={breadcrumb.url}>
                    {breadcrumb.name}
                </Link>
            ))}
            <Typography color="text.primary">{primaryName}</Typography>
        </Breadcrumbs>
    );
}
export default BreadCrumbsComponent;