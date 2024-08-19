import React, { useState, useRef } from "react";
import MenuItem from "../data/MenuItem";
import { Link } from "react-router-dom";

const Dropdown = () => {
  const [openIndex, setOpenIndex] = useState(null);
  const [subOpenIndex, setSubOpenIndex] = useState(null);
  const closeTimeout = useRef(null);

  const handleMouseEnter = (index) => {
    clearTimeout(closeTimeout.current);
    setOpenIndex(index);
  };

  const handleMouseLeave = () => {
    closeTimeout.current = setTimeout(() => {
      setOpenIndex(null);
    }, 500);
  };

  const handleSubMouseEnter = (index) => {
    clearTimeout(closeTimeout.current);
    setSubOpenIndex(index);
  };

  const handleSubMouseLeave = () => {
    closeTimeout.current = setTimeout(() => {
      setSubOpenIndex(null);
    }, 500);
  };
  /*reder menu dropdown*/
  const RenderMenu = () => {
    return (
      <ul className="flex gap-4 relative">
        {MenuItem.map((item, index) => (
          <li
            key={index}
            onMouseEnter={() => handleMouseEnter(index)}
            onMouseLeave={handleMouseLeave}
            className="relative cursor-pointer"
          >
            {item.link ? (
              <Link
                to={item.link}
                className=" hover:text-gray-200 font-semibold text-xl"
              >
                {item.title}
              </Link>
            ) : (
              <span className="hover:text-gray-200 font-semibold cursor-pointer text-xl">
                {item.title}
              </span>
            )}
            {item.submenus && (
              <ul
                className={`absolute left-0 mt-2 bg-white border border-gray-300 rounded-lg shadow-lg ${
                  openIndex === index ? "block" : "hidden"
                }`}
                onMouseEnter={() => clearTimeout(closeTimeout.current)}
                onMouseLeave={handleMouseLeave}
              >
                {item.submenus.map((subItem, subIndex) => (
                  <li
                    key={subIndex}
                    onMouseEnter={() => handleSubMouseEnter(subIndex)}
                    onMouseLeave={handleSubMouseLeave}
                    className="relative cursor-pointer"
                  >
                    {subItem.link ? (
                      <Link
                        to={subItem.link}
                        className="block px-4 py-2 text-gray-700 hover:bg-gray-100"
                      >
                        {subItem.title}
                      </Link>
                    ) : (
                      <span className="block px-4 py-2 text-gray-700 hover:bg-gray-100">
                        {subItem.title}
                      </span>
                    )}
                    {subItem.submenus && (
                      <ul
                        className={`absolute left-full top-[-50%] mt-2 bg-white border border-gray-300 rounded-lg shadow-lg ${
                          subOpenIndex === subIndex ? "block" : "hidden"
                        }`}
                        onMouseEnter={() => clearTimeout(closeTimeout.current)}
                        onMouseLeave={handleSubMouseLeave}
                      >
                        {subItem.submenus.map((minisubItem, minisubIndex) => (
                          <li key={minisubIndex}>
                            <Link
                              to={minisubItem.link}
                              className="block px-4 py-2 text-gray-700 hover:bg-gray-100"
                            >
                              {minisubItem.title}
                            </Link>
                          </li>
                        ))}
                      </ul>
                    )}
                  </li>
                ))}
              </ul>
            )}
          </li>
        ))}
      </ul>
    );
  };

  return (
    <>
      <RenderMenu />
    </>
  );
};

export default Dropdown;
