import React from "react";
import "./SwitchTheme.css";
import {useTheme} from "../../context/ThemeProvider";

const Switch = () => {
    const { isDarkMode, toggleDarkMode } = useTheme();

    const onToggle = () => {
        toggleDarkMode(!isDarkMode);
    };

    return (
        <label className="toggle-switch">
            <input type="checkbox" checked={isDarkMode} onChange={onToggle} />
            <span className="switch" />
        </label>
    );
}
export default Switch;