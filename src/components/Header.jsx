import Dropdown from "./Dropdown";
import { FaBars, FaTimes } from "react-icons/fa";
import MenuItem from "../data/MenuItem";
import { useState } from "react";
import { Link } from "react-router-dom";

const Header = () => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  //tạo menu nhỏ cho màn hình nhỏ
  const RenderMiniMenu = () => {
    return (
      <div className="menu h-[100dvh] min-w-56 overflow-auto z-50 bg-white border-2">
        <ul>
          {MenuItem.map((item, index) => (
            <li key={index} className="mt-2">
              {item.link ? (
                <Link to={item.link} className="text-xl">
                  {item.title}
                </Link>
              ) : (
                <details>
                  <summary className="text-xl">{item.title}</summary>
                  <ul>
                    {item.submenus.map((subitem, subindex) => (
                      <li key={subindex} className="mt-2">
                        {subitem.link ? (
                          <Link to={subitem.link} className="text-lg">
                            {subitem.title}
                          </Link>
                        ) : (
                          <details>
                            <summary className="text-lg">
                              {subitem.title}
                            </summary>
                            <ul>
                              {subitem.submenus.map(
                                (subsubitem, subsubindex) => (
                                  <li key={subsubindex} className="mt-2">
                                    <Link
                                      to={subsubitem.link}
                                      className="text-md"
                                    >
                                      {subsubitem.title}
                                    </Link>
                                  </li>
                                )
                              )}
                            </ul>
                          </details>
                        )}
                      </li>
                    ))}
                  </ul>
                </details>
              )}
            </li>
          ))}
        </ul>
      </div>
    );
  };

  return (
    <header className="flex items-center h-16 bg-white-500 shadow-lg">
      <div className="container mx-auto p-3 flex justify-between items-center">
        <img src="/s4mlogoall.png" alt="logo" className="md:h-8 lg:h-14 h-7" />
        <div className="text-lg font-semibold md:text-2xl lg:text-4xl">
          CONDITION MONITORING
        </div>
        {/* hiển thị menu dropdown khi màn hình lớn hơn md */}
        <div className="hidden md:block">
          <Dropdown />
        </div>
        {/* hiển thị menu nhỏ khi màn hình nhỏ hơn md */}
        <div className="md:hidden relative">
          {isMenuOpen ? (
            <FaTimes
              className="text-2xl"
              onClick={() => setIsMenuOpen(!isMenuOpen)}
            />
          ) : (
            <FaBars
              className="text-2xl"
              onClick={() => setIsMenuOpen(!isMenuOpen)}
            />
          )}
          {isMenuOpen && (
            <div className="absolute right-0 mt-[100%]">
              <RenderMiniMenu />
            </div>
          )}
        </div>
      </div>
    </header>
  );
};

export default Header;
